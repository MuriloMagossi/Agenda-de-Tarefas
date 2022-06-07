using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTarefas.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }

        public int Priority { get; set; } = 0;

        public Boolean Checked { get; set; } = false;

        [Required(ErrorMessage = "Título é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Dscrição é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Data é obrigatório")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Horário de inicio é obrigatório")]
        [DataType(DataType.Time)]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "Horário de fim é obrigatório")]
        [DataType(DataType.Time)]
        public string EndTime { get; set; }

        // [StringLength(50, ErrorMessage = "Use menos caracteres")]
    }
}