namespace TarefaProcessorApi.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; } = "PENDING";
        public DateTime? DataInicioProcessamento { get; set; }
        public DateTime? DataFimProcessamento { get; set; }
    }
}

