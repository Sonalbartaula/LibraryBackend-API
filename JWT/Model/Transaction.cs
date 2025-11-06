using Jwt.Model;
using JWT.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace JWT.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string MemeberName { get; set; }

        public string Isbn { get; set; }

        public string Booktitle { get; set; }

        public DateTime CheckoutDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);

        public DateTime? ReturnDate { get; set; }
        public Issuestatus IssueStatus { get; set; } = Issuestatus.Issued;

        public decimal Fine { get; set; } = 0;

        [NotMapped]
        public string Status
        {
            get
            {
                if (ReturnDate.HasValue)
                    return "Returned";
                if (DateTime.UtcNow > DueDate)
                    return "Overdue";
                return "Active";
            }
        }

        [NotMapped]
        public string TimeLeft
        {
            get
            {
                if (ReturnDate.HasValue)
                    return "Returned";
                var remaining = (DueDate - DateTime.UtcNow).TotalDays;
                return remaining > 0
                    ? $"{Math.Ceiling(remaining)} days left"
                    : $"{Math.Abs(Math.Ceiling(remaining))} days overdue";
            }
        }
    }
}

