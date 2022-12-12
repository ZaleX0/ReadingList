import React, { useState } from 'react'
import { Form, Button } from 'react-bootstrap'

function AddAuthor() {
  const [authorName, setAuthorName] = useState("");

  return (
    <>
      <h1 className="display-4">Add Author</h1>
      {renderForm()}
    </>
  )

  function renderForm() {
    return (
      <Form onSubmit={postAuthor}>
        <Form.Label>Full name</Form.Label>
        <Form.Control className="mb-2" type="text" name="title" value={authorName} onChange={event=>setAuthorName(event.target.value)} required />
        <Button variant="success" type="submit">Add author</Button>
      </Form>
    )
  }

  function postAuthor() {
    fetch('/api/author', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        fullName: authorName
      })
    })
  }
}

export default AddAuthor