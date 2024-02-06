using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab4.Models
{
    public class GuestMetadata
    {
        [Display(Name = "ФИО гостя")]
        public string GUEST_NAME { get; set; }
    }
    [MetadataType(typeof(GuestMetadata))]
    public partial class Guest
    {
    }
    public class RoomMetadata
    {
        [Display(Name = "Название комнаты")]
        public string ROOM_NAME { get; set; }
    }
    [MetadataType(typeof(RoomMetadata))]
    public partial class Room
    {
    }
    public class GuestsInRoomMetadata
    {
        [Display(Name = "Комната")]
        public int ROOM_ID { get; set; }
        [Display(Name = "Гость")]
        public int GUEST_ID { get; set; }
    }
    [MetadataType(typeof(GuestsInRoomMetadata))]
    public partial class GuestsInRoom
    {

    }
}