using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sjukhus_journal.Models;

public class Journal
{
    [Key]
    public int JournalId { get; set; }
    
    [Required]
    public int PatientId { get; set; }

    public String Anteckning{ get; set; }
}