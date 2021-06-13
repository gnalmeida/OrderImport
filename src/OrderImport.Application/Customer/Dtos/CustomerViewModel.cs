using System.ComponentModel.DataAnnotations;

namespace OrderImport.Application.Customer.Dtos
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string CPF { get; set; }
    }
}
