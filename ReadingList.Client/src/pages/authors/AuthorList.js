import React, { useEffect, useState } from 'react'
import { Button, Form, Modal, Table } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function AuthorList() {
  const [authors, setAuthors] = useState([]);

  const [author, setAuthor] = useState([]);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);

  function handleChange(event) {
    setAuthor({
      id: author.id,
      fullName: event.target.value
    })
  }

  function openEditModal(author) {
    setIsEditModalOpen(true);
    setAuthor(author);
  }
  function closeEditModal() {
    setIsEditModalOpen(false);
  }
  function openDeleteModal(author) {
    setIsDeleteModalOpen(true);
    setAuthor(author);
  }
  function closeDeleteModal() {
    setIsDeleteModalOpen(false);
  }

  useEffect(() => {
    getAuthors();
  }, [])

  return (
    <>
      <div className="d-flex justify-content-between align-items-center">
        <h1 className="display-4">Authors</h1>
        <Link to="/authors/add">
          <Button variant="outline-primary">Add new author</Button>
        </Link>
      </div>
      {renderTable()}
      {editModal()}
      {deleteModal()}
    </>
  )

  function renderTable() {
    return (
      <Table className="table-striped border">
        <thead className="table-primary">
          <tr>
            <th>Full name</th>
            <th>Options</th>
          </tr>
        </thead>
        <tbody>
          {authors.map(author => 
            <tr key={author.id}>
              <td>
                {author.fullName}
              </td>
              <td>
                <Button onClick={()=>openEditModal(author)} variant="outline-primary">Edit</Button>
                <Button onClick={()=>openDeleteModal(author)} variant="outline-danger" className="ms-2">Delete</Button>
              </td>
            </tr>
          )}
        </tbody>
      </Table>
    )
  }

  function editModal() {
    return (
      <Modal show={isEditModalOpen} onHide={closeEditModal}>
        <Modal.Header>
          <Modal.Title>Edit</Modal.Title>
        </Modal.Header>
        <Form onSubmit={updateAuthor}>
          <Modal.Body>
            <Form.Label>Full name</Form.Label>
            <Form.Control className="mb-2" type="text" name="title" value={author.fullName} onChange={handleChange} required />
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={closeEditModal}>Close</Button>
            <Button className="btn-success" type="submit">Update</Button>
          </Modal.Footer>
        </Form>
      </Modal>
    )
  }

  function deleteModal() {
    return (
      <Modal show={isDeleteModalOpen} onHide={closeDeleteModal}>
        <Modal.Header>
          <Modal.Title>Delete</Modal.Title>
        </Modal.Header>
        <Form onSubmit={deleteAuthor}>
          <Modal.Body>
            <p>Are you sure to delete '{author.fullName}' from database?</p>
            <p className="text-danger">This will also remove ALL his/her books!</p>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={closeDeleteModal}>Close</Button>
            <Button className="btn-danger" type="submit">Delete</Button>
          </Modal.Footer>
        </Form>
      </Modal>
    )
  }

  function getAuthors() {
    fetch('/api/author', {method: 'GET'})
      .then(response => response.json())
      .then(json => setAuthors(json));
  }

  function updateAuthor() {
    fetch('/api/author/' + author.id, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        fullName: author.fullName
      })
    });
  }

  function deleteAuthor() {
    console.log(author);
    fetch('/api/author/' + author.id, {
      method: 'DELETE'
    });
  }
}

export default AuthorList