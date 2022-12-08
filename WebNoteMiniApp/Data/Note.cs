using System.ComponentModel.DataAnnotations;

namespace WebNoteMiniApp.Data
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string OwnerId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? Created { get; set; }
    }
}
