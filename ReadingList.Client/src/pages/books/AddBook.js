import React, { useEffect, useState } from 'react'
import { Button, Form } from 'react-bootstrap'
import { Link } from 'react-router-dom';

function AddBook() {
  const [authors, setAuthors] = useState([]);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [authorId, setAuthorId] = useState(1);

  useEffect(() => {
    getAuthors();
  }, [])

  return (
    <>
      <h1 className="display-4">Add book</h1>
      {renderAddBookForm()}
    </>
  )

  function handleTitleChange(event) {
    setTitle(event.target.value);
  }
  function handleDescriptionChange(event) {
    setDescription(event.target.value);
  }
  function handleAuthorIdChange(event) {
    setAuthorId(event.target.value);
  }

  function getAuthors() {
    fetch('/api/author', {
      method: 'GET'
    })
    .then(response => response.json())
    .then(json => {
      setAuthors(json);
    });
  }

  function postBook() {
    fetch('/api/book', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        authorId: authorId,
        title: title,
        description: description
      })
    });
  }

  function renderAddBookForm() {
    return (
      <div className="form-group">
        <Form onSubmit={postBook}>
          <Form.Label>Author</Form.Label>
          <div className="d-flex justify-content-between">
            <Form.Select className="mb-2" name="authorId" value={authorId} onChange={handleAuthorIdChange}>
              {authors.map(author =>
                <option key={author.id} value={author.id}>
                  {author.fullName}
                </option>    
              )}
            </Form.Select>
            <Link to={"/authors/add"}>
              <Button className="ms-2">Add</Button>
            </Link>
          </div>
          <Form.Label>Title</Form.Label>
          <Form.Control className="mb-2" type="text" name="title" value={title} onChange={handleTitleChange} required />
          <Form.Label>Description</Form.Label>
          <Form.Control className="mb-2" type="text" name="description" value={description} onChange={handleDescriptionChange}/>
          
          <Button variant="success" type="submit">Add book</Button>
        </Form>
      </div>
    )
  }
}

export default AddBook