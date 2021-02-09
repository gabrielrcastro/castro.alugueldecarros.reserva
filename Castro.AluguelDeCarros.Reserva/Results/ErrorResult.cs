using FluentValidation.Results;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.API.Results
{
    public class ErrorResult
    {
        private readonly List<ErrorModel> objectResult;

        public ErrorResult(List<ValidationFailure> fluntFailure)
        {
            objectResult = new List<ErrorModel>();
            foreach (var failure in fluntFailure)
            {
                objectResult.Add(new ErrorModel
                {
                    Propriedade = failure.PropertyName,
                    Mensagem = failure.ErrorMessage
                });
            }
        }

        public ErrorResult(IEnumerable<List<ValidationFailure>> fluntFailure)
        {
            objectResult = new List<ErrorModel>();
            foreach (var listFailure in fluntFailure)
                foreach (var failure in listFailure)
                {
                    objectResult.Add(new ErrorModel
                    {
                        Propriedade = failure.PropertyName,
                        Mensagem = failure.ErrorMessage
                    });
                }
        }

        public List<ErrorModel> ToResult()
        {
            return objectResult;
        }
    }

    public class ErrorModel
    {
        public string Propriedade { get; set; }
        public string Mensagem { get; set; }
    }
}
