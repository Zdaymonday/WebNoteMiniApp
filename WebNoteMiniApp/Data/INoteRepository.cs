namespace WebNoteMiniApp.Data
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAll();
        Task<Note> GetById(int id);
        Task Add(Note note);
        Task Update(Note note);
        Task Remove(int id);
    }
}
