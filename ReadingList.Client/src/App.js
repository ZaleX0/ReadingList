import React from 'react'
import { Route, Routes } from 'react-router-dom'
import Book from './pages/books/Book'
import BookList from './pages/books/BookList'
import AddBook from './pages/books/AddBook'
import AddAuthor from './pages/authors/AddAuthor'
import ReadingList from './pages/reading/ReadingList'
import Home from './pages/Home'
import NotFound from './pages/NotFound'
import Navbar from 'react-bootstrap/Navbar'
import Nav from 'react-bootstrap/Nav'
import 'bootstrap/dist/css/bootstrap.min.css'
import { LinkContainer } from 'react-router-bootstrap'
import AuthorList from './pages/authors/AuthorList'
import { House, Book as BookIcon, Person, CardList } from 'react-bootstrap-icons'

function App() {
  return (
    <div className="container">
      {renderNav()}
      {defineRoutes()}
    </div>
  )

  function defineRoutes() {
    return (
      <Routes>
        <Route path="*" element={<NotFound />}/>
        <Route path="/" element={<Home />} />
        <Route path="/books">
          <Route index element={<BookList />} />
          <Route path=":id" element={<Book />} />
          <Route path="add" element={<AddBook />} />
        </Route>
        <Route path="/authors">
          <Route index element={<AuthorList />} />
          <Route path="add" element={<AddAuthor />} />
        </Route>
        <Route path="/reading" element={<ReadingList />} />
      </Routes>
    )
  }

  function renderNav() {
    return (
      <Navbar>
        <Nav>
          <LinkContainer to="/">
            <Nav.Link><House/> Home</Nav.Link>
          </LinkContainer>
          <LinkContainer to="/books">
            <Nav.Link><BookIcon/> Books</Nav.Link>
          </LinkContainer>
          <LinkContainer to="/authors">
            <Nav.Link><Person/> Authors</Nav.Link>
          </LinkContainer>
          <LinkContainer to="/reading">
            <Nav.Link><CardList/> Reading list</Nav.Link>
          </LinkContainer>
        </Nav>
      </Navbar>
    )
  }
}

export default App