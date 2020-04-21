namespace MedicalSystem.Gateways.WebGateway.Models
{
    public class ErrorModel
    {
        public string Reason { get; set; }

        public ErrorModel(string reason)
        {
            Reason = reason;
        }
    }
}
