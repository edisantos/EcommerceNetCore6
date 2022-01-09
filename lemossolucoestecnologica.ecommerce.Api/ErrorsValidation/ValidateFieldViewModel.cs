namespace lemossolucoestecnologica.ecommerce.Api.ErrorsValidation
{
    public class ValidateFieldViewModel
    {
        public IEnumerable<string>? Erros { get; set; }

        public ValidateFieldViewModel(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
