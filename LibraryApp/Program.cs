using LibraryApp;

Console.WriteLine("=== LibraryApp: учёт книг ===");

Library library = new();

library.AddBook(new Book { Id = 1, Title = "Мёртвые души", Author = "Н. В. Гоголь" });
library.AddBook(new Book { Id = 2, Title = "Преступление и наказание", Author = "Ф. М. Достоевский" });
library.AddBook(new Book { Id = 3, Title = "Война и мир", Author = "Л. Н. Толстой" });

Console.WriteLine();

Console.WriteLine("-- Корректные операции --");
library.BorrowBook(1);          // выдача доступной книги
library.ReturnBook(1);          // возврат выданной книги

Console.WriteLine();
Console.WriteLine("-- Тестирование граничных случаев --");

library.BorrowBook(99);         // BUG-1: книга не существует
library.BorrowBook(2);
library.BorrowBook(2);          // BUG-2: повторная выдача
library.ReturnBook(3);          // BUG-3: возврат невыданной книги

Console.WriteLine();
Console.WriteLine("Программа завершилась без исключений.");
