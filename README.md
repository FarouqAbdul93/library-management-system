# Library Management System

A console-based Library Management System built in C# (.NET 8). Users can add, search, borrow, and return books, and the library's state is saved between sessions.

## How to Run

1. Make sure you have the [.NET 8 SDK](https://dotnet.microsoft.com/download) installed.
2. Clone this repository:

git clone https://github.com/FarouqAbdul93/library-management-system.git

3. Open `LibraryManagementSystem.sln` in Visual Studio 2022 (or later).
4. Make sure `LibraryManagementSystem` is set as the startup project (it should be by default).
5. Run the project (F5 or Ctrl+F5).

Alternatively, from the command line:

cd LibraryManagementSystem/LibraryManagementSystem
dotnet run


### Running the tests

Open Test Explorer in Visual Studio and click "Run All Tests", or run from the command line:

cd LibraryManagementSystem.Tests
dotnet test


## Features Implemented

### Core Requirements
- `Book` class with title, author, and borrowed status
- `Library` class managing a collection of books (add, search, borrow, return, view available)
- Console menu covering all required operations

### Mandatory Extras
- **Unit tests** for both `Book` and `Library` (25 tests, NUnit), written test-first
- **Exception handling**: trying to borrow or return a book that doesn't exist throws a `KeyNotFoundException`, which the console menu catches and displays as a message rather than crashing. Trying to borrow a book that's already borrowed, or return one that isn't borrowed, just returns `false`, since those aren't really errors, just expected "can't do that right now" outcomes.
- **Persistence**: the library is saved to `library.json` when the user exits, and reloaded automatically next time the app runs, including each book's borrowed status.

### Extra Intermediate
- **Search by title or author**: `SearchBooks(searchTerm)` checks both fields and returns all matches.

### Extra Advanced
- **Sorting**: books can be viewed sorted alphabetically by title or by author.

### Extra (beyond the spec)
- **Case-insensitive matching**: searching, borrowing, and returning all ignore case, so "harry potter" matches "Harry Potter". This wasn't asked for, but exact-case matching would make the search feature frustrating to actually use.

### Not implemented
- **File-based database**: a JSON file was used instead of something like SQLite. This was a deliberate choice given the scope of the exercise, JSON already satisfies the "save and reload" requirement, and a full database felt like unnecessary complexity for a project this size.

## Design Decisions

**Why `SearchBook` returns `null`, but `BorrowBook`/`ReturnBook` throw exceptions when a title doesn't exist:**
Not finding something during a search is a normal outcome, similar to a search engine returning no results. But trying to borrow or return a book that doesn't exist at all suggests something's actually gone wrong (a typo, or a bug passing in the wrong title), so that's treated as an exception rather than quietly returning `false`.

**Why `SearchBooks` returns a `List<Book>` instead of a single `Book`:**
The original spec's `searchBook(title)` returning one result makes sense for titles, since they're usually unique. But once searching by author was added, one result stopped making sense, an author can have many books, and returning just the first match would mean genuinely useful results get thrown away. A list (empty if nothing matches) handles that properly.

**Encapsulation in `Book`:**
`IsBorrowed` only has a private setter, so it can only change through `BorrowBook()`/`ReturnBook()`, which enforce the actual rules (you can't borrow something already borrowed). `Title` and `Author` can't be changed after a book is created, since a book's identity shouldn't change.

**Test-driven throughout:**
Every method was written test-first: a failing test was written to describe the behaviour before the method existed, then just enough code was added to make it pass.