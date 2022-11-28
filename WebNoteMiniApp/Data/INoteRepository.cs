namespace WebNoteMiniApp.Data
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();
        Task<IEnumerable<Note>> GetByTitleAsync(string title);
        Task<Note> GetByIdAsync(int id);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task RemoveAsync(int id);
    }
}
