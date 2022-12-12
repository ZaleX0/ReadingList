import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { CheckLg, Dash } from 'react-bootstrap-icons';

function BookList() {
  const [books, setBooks] = useState([]);
  const [search, setSearch] = useState("");

  useEffect(() => {
    getBooks();
  }, []);

  return (
    <>
      <div className="d-flex justify-content-between align-items-center">
        <h1 className="display-4">Books</h1>
        <div className="d-flex justify-content-between align-items-center">
          <label>Search</label>
          <input className="form-control ms-2" type="text" value={search} onChange={event => setSearch(event.target.value)} />
        </div>
        <Link to="/books/add">
          <Button variant="outline-primary">Add new book</Button>
        </Link>
      </div>
      {renderBooksTable()}
    </>
  );

  function getBooks() {
    fetch('/api/book', {
      method: 'GET'
    })
    .then(response => response.json())
    .then(json => {
      setBooks(json);
    });
  }
  function renderBooksTable() {
    let data = books.filter(
      b =>
      b.title.toLowerCase().includes(search.toLowerCase()) || 
      b.author.toLowerCase().includes(search.toLowerCase())
    );
    return (
      <Table className="table-striped border">
        <thead className="table-primary">
          <tr>
            <th>Title</th>
            <th>Author</th>
            <th>Read</th>
          </tr>
        </thead>
        <tbody>
          {data.map(book => 
            <tr key={book.id}>
                <td>
                  <Link to={"/books/" + book.id}>
                    {book.title}
                  </Link>
                </td>
                <td>
                    {book.author}
                </td>
              <td>
                <div>{book.isRead ? <CheckLg color="green" /> : <Dash />}</div>
              </td>
            </tr>
          )}
        </tbody>
      </Table>
    )
  }
}

export default BookList