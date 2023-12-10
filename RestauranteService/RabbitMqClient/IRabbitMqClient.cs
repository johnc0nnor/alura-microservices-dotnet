using RestauranteService.Dtos;

namespace RestauranteService.RabbitMqClient;

public interface IRabbitMqClient
{
    void PublicarRestaurante(RestauranteReadDto restauranteReadDto);
}
