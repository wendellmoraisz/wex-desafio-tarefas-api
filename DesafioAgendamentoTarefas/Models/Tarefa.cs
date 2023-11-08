namespace DesafioAgendamentoTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EStatusTarefa Status { get; set; }
    }
}
