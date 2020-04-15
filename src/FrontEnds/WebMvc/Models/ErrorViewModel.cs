namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel()
        {
            RequestId = string.Empty;
        }
    }
}
