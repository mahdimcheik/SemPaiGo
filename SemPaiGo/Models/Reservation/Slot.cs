using SemPaiGo.Models.Interfaces;

namespace SemPaiGo.Models
{
    public class Slot : BaseModel
    {
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public Guid TeacherId { get; set; }
        public UserApp Teacher { get; set; }
        public Guid TypeId { get; set; }
        public TypeSlot Type { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
