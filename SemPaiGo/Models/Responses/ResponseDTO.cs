using System.ComponentModel.DataAnnotations;

namespace SemPaiGo.Models;

public class ResponseDTO<T>
{
    [Required]
    public string Message { get; set; }
    [Required]
    public int Status { get; set; }
    public T? Data { get; set; }
    public long? Count { get; set; }
}
