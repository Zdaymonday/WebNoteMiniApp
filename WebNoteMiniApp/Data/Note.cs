namespace WebNoteMiniApp.Data
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? Created { get; set; } = DateTime.Now.ToString();
    }
}
