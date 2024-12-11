using Core.DtoModels;
using Core.Services.FileService;
using DataAccess.Models;
using DataAccess.Repositories.BookRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.BookService
{
    public class BookService
    {
        private readonly IBookRepository _bookrepository;
        private readonly IFileUploadService _uploadservice;
        public BookService(IBookRepository bookrepository, IFileUploadService uploadservice)
        {
            _bookrepository = bookrepository;
            _uploadservice = uploadservice;
        }
        public async Task<IEnumerable<Book>> GetAllBooks() 
        { 
            return await _bookrepository.GetAll().ToListAsync();
        } 
        public async Task<IEnumerable<Book>> GetAllBooksWithAuthor(Expression<Func<Book, bool>> where = null)
        {
            return await _bookrepository.GetAll(where).Include(x => x.Author).ToListAsync();
        }
        public async Task CreateBook(BookDto bookdto)
        {
            var book = new Book()
            {
                AuthorId = bookdto.AuthorId,
                Title = bookdto.Title,
                Description = bookdto.Description,
                Price = bookdto.Price,
                isActive = bookdto.isActive,
                inHomePage = bookdto.inHomePage,
            };

            if (bookdto.Img != null)
            {
                book.Img = await _uploadservice.UploadFileAsync(bookdto.Img);
            }

            await _bookrepository.Create(book);
        }
        public async Task UpdateBook(BookDto bookDto)
        {
            var book = await _bookrepository.GetById(bookDto.Id);

            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;
            book.AuthorId = bookDto.AuthorId;
            book.isActive = bookDto.isActive;
            book.inHomePage = bookDto.inHomePage;

            if (bookDto.Img != null)
            {
                book.Img = await _uploadservice.UploadFileAsync(bookDto.Img);

            }

            await _bookrepository.Update(book);
        }
        public async Task DeleteBook(Book book)
        {
            await _bookrepository.Delete(book);
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _bookrepository.GetById(id);
        }
        public async Task<BookDto> GetBookDtoById(int id)
        {
            var book = await _bookrepository.GetById(id);
            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImgName = book.Img,
                AuthorId = book.AuthorId
            };

            return bookDto;
        }
    }
}
