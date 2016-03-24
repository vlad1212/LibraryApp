# LibraryApp

This is a basic app that allows assignment of books to users, book search by Author/Title/ISBN, and book check in.

Befotre running the application, Library database and all its objects must be created on the database server. A SQL script is included to create required tables, views, and stored procedures. A SQL account LibAdmin must also be created and needs to have read/write permissions to the Library DB, as well as, execute rights on all stored procedures. Also make sure to change the connection string in Web.config file to match you SQL configuration. The app does not provide an interface for adding new users or books so a few records will need to be created in both Users and Books tables manually.

User interface is on Default.aspx page. When the page is loaded, a list of users will show up in the top left corner. Users can be created with no library card assgined. When a user is selected, a table below will show any outstanding books he/she currently has. The lower table will aslo show the checkout date for each book. That second table will also give you the ability to check the books back in. 

A section in the top right corner of the page allows you to search for books and assign them to users. All books that match your search parameter will show up in the list - even if they are checked out. Checked out books will have IsAvailable column unchecked and will not be availabe to check out until checked in. Users must have a value in CarrdNumber to check out a book.
