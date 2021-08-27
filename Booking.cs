//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel_Booking_App
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Evaluated = false;
        }
    
        public int Id { get; set; }
        public System.DateTime CheckIn { get; set; }
        public System.DateTime CheckOut { get; set; }
        public short PersonsNumber { get; set; }
        public double AmountPaid { get; set; }
        public System.DateTime DateMade { get; set; }
        public bool Evaluated { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> RoomId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
