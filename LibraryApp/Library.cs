namespace LibraryApp;

/// <summary>
/// Библиотека: хранит книги и управляет их выдачей и возвратом.
/// </summary>
public class Library
{
    private readonly List<Book> _books = new();

        /// <param name="book">Добавляемая книга.</param>
    /// <exception cref="ArgumentNullException">Объект книги не передан.</exception>
    /// <exception cref="ArgumentException">Книга с таким Id уже существует.</exception>
    public void AddBook(Book book)
    {
    
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book), "Книга не может быть null.");
        }

        // BUG-4 (исправлен): ранее можно было добавить две книги с одним Id.
        if (_books.Any(b => b.Id == book.Id))
        {
            throw new ArgumentException($"Книга с Id = {book.Id} уже существует.", nameof(book));
        }

        _books.Add(book);
        Console.WriteLine($"Добавлена книга: \"{book.Title}\" (Id = {book.Id}).");
    }

    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Найденная книга или null, если книги с таким Id нет.</returns>
    public Book? FindBook(int id)
    {
        // Точка останова ставилась здесь для проверки сценария BUG-1.
        return _books.FirstOrDefault(b => b.Id == id);
    }

   
    /// <param name="id">Идентификатор книги.</param>
    public void BorrowBook(int id)
    {
        
        Book? book = FindBook(id);
        if (book is null)
        {
            Console.WriteLine($"Ошибка: книга с Id = {id} не найдена.");
            return;
        }

        // BUG-2 (исправлен): ранее книга выдавалась повторно,
        // даже если IsAvailable уже был false.
        if (!book.IsAvailable)
        {
            Console.WriteLine($"Ошибка: книга \"{book.Title}\" уже выдана.");
            return;
        }

        book.IsAvailable = false;
        Console.WriteLine($"Книга \"{book.Title}\" выдана.");
    }

       /// <param name="id">Идентификатор книги.</param>
    public void ReturnBook(int id)
    {
        Book? book = FindBook(id);
        if (book is null)
        {
            Console.WriteLine($"Ошибка: книга с Id = {id} не найдена.");
            return;
        }

        // BUG-3 (исправлен): ранее возврат невыданной книги менял её состояние
        // без какой-либо диагностики.
        if (book.IsAvailable)
        {
            Console.WriteLine($"Ошибка: книга \"{book.Title}\" не была выдана, возврат невозможен.");
            return;
        }

        book.IsAvailable = true;
        Console.WriteLine($"Книга \"{book.Title}\" возвращена в библиотеку.");
    }
}
