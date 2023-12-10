namespace ItemService.EventProcessor;

public interface IProcessorEvent
{
    void Processa(string mensagem);
}
