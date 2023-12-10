using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using System.Text.Json;

namespace ItemService.EventProcessor;

public class ProcessorEvent : IProcessorEvent
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProcessorEvent(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public void Processa(string mensagem)
    {
        using var scope = _scopeFactory.CreateScope();
        
        var itemRepository = scope.ServiceProvider.GetRequiredService<ItemRepository>();

        var restauranteReadDto = JsonSerializer.Deserialize<RestauranteReadDto>(mensagem);

        var restaurante = _mapper.Map<Restaurante>(restauranteReadDto);

        if(!itemRepository.ExisteRestauranteExterno(restaurante.Id))
        {
            itemRepository.CreateRestaurante(restaurante);
            itemRepository.SaveChanges();
        }
    }
}
