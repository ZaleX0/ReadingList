import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import Button from 'react-bootstrap/Button'
import 'bootstrap/dist/css/bootstrap.min.css'
import { Form, Modal } from 'react-bootstrap'
import { Link } from 'react-router-dom'

function Book() {
  const { id } = useParams();
  const [book, setBook] = useState([]);
  const [readState, setReadState] = useState();

  const [isOnReadingList, setIsOnReadingList] = useState(false);

  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [authors, setAuthors] = useState([]);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [authorId, setAuthorId] = useState(1);
  
  const toggleRead = () => {
    readState ? markAsUnread() : markAsRead();
    setReadState(!readState);
  }

  const addToReadingList = () => {
    setIsOnReadingList(!isOnReadingList)
    postToReadingList();
  }

  useEffect(() => {
    getBook(id);
    checkIfOnReadingList(id);
  }, [id])
  
  return (
    <>
      {renderButtons()}
      <h1 className="display-4">{book.title}</h1>
      Author - {book.author}
      <p>{book.description}</p>
      {editModal()}
      {deleteModal()}
    </>
  )

  function renderButtons() {
    return (
      <div className="d-flex justify-content-between align-items-center">
        <div>
          <Button onClick={addToReadingList} variant="outline-primary" className="ms-2" disabled={isOnReadingList}>
            {isOnReadingList ? <>Added to reading list</> : <>Add to reading list</>}
          </Button>
          <Button onClick={toggleRead} variant={readState ? "outline-secondary" : "outline-success" } className="ms-2">
            {readState ? <>Mark as unread</> : <>Mark as read</>}
          </Button>
        </div>
        <div>
          <Button onClick={() => {setIsEditModalOpen(true); getAuthors()}} variant="outline-primary" className="ms-2">Edit</Button>
          <Button onClick={() => setIsDeleteModalOpen(true)} variant="outline-danger" className="ms-2">Delete</Button>
        </div>
      </div>
    )
  }

  function editModal() {
    return (
      <Modal show={isEditModalOpen} onHide={() => setIsDeleteModalOpen(false)}>
        <Modal.Header>
          <Modal.Title>Edit</Modal.Title>
        </Modal.Header>
        <Form onSubmit={updateBook}>
          <Modal.Body>
              <Form.Label>Author</Form.Label>
              <div className="d-flex justify-content-between">
                <Form.Select className="mb-2" name="authorId" value={authorId} onChange={event => setAuthorId(event.target.value)}>
                  {authors.map(author =>
                    <option key={author.id} value={author.id} selected={author.id === authorId}>
                      {author.fullName}
                    </option>    
                  )} 
                </Form.Select>
                <Link to={"/authors/add"}>
                  <Button className="ms-2">Add</Button>
                </Link>
              </div>
              <Form.Label>Title</Form.Label>
              <Form.Control className="mb-2" type="text" name="title" value={title} onChange={event => setTitle(event.target.value)} required />
              <Form.Label>Description</Form.Label>
              <Form.Control className="mb-2" type="text" name="description" value={description} onChange={event => setDescription(event.target.value)}/>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => setIsEditModalOpen(false)}>Close</Button>
            <Button className="btn-success" type="submit">Update</Button>
          </Modal.Footer>
        </Form>
      </Modal>
    )
  }
  function deleteModal() {
    return (
      <Modal show={isDeleteModalOpen} onHide={() => setIsDeleteModalOpen(false)}>
        <Modal.Header>
          <Modal.Title>Delete</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure to delete '{book.title}' from database?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setIsDeleteModalOpen(false)}>Close</Button>
          <Link to="/books">
            <Button variant="danger" onClick={() => deleteBook()}>Delete</Button>
          </Link>
        </Modal.Footer>
      </Modal>
    )
  }

  

  function getAuthors() {
    fetch('/api/author', {
      method: 'GET'
    })
    .then(response => response.json())
    .then(json => {
      setAuthors(json);
      setTitle(book.title);
      setDescription(book.description);
      setAuthorId(book.authorId);
    });
  }

  function getBook(id) {
    fetch(`/api/book/${id}`, {
      method: 'GET'
    })
    .then(response => response.json())
    .then(json => {
      setBook(json);
      setReadState(json.isRead);
    });
  }

  function updateBook() {
    fetch(`/api/book/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        authorId: authorId,
        title: title,
        description: description
      })
    });
  }

  function deleteBook() {
    fetch(`/api/book/${id}`, { method: 'DELETE' });
  }

  function checkIfOnReadingList(id) {
    fetch(`/api/bookpriority/${id}`, { method: 'GET' })
      .then(response => response.json())
      .then(json => setIsOnReadingList(json));
  }

  function postToReadingList() {
    fetch(`/api/bookpriority/${id}`, { method: 'POST' });
  }

  function markAsRead() {
    fetch(`/api/bookread/${id}`, { method: 'POST' });
  }

  function markAsUnread() {
    fetch(`/api/bookread/${id}`, { method: 'DELETE' });
  }
}

export default Book