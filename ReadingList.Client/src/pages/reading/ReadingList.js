import { Button } from 'react-bootstrap';
import React, { useEffect, useState } from 'react'
import { DragDropContext, Draggable, Droppable } from 'react-beautiful-dnd';
import { Table } from 'react-bootstrap';
import { List } from 'react-bootstrap-icons';
import { Link } from 'react-router-dom';

function ReadingList() {
  const [booksPrority, setBooksPrority] = useState([])
  const [disabledSaveButton, setDisabledSaveButton] = useState(true);

  const handleDragEnd = (result) => {
    if (!result.destination) return;
    let tempBooksPriority = [...booksPrority];
    let [selectedRow] = tempBooksPriority.splice(result.source.index, 1);
    tempBooksPriority.splice(result.destination.index, 0, selectedRow);
    setBooksPrority(tempBooksPriority);
    setDisabledSaveButton(false);
  }

  useEffect(() => {
    getBookPriorityList();
  }, [])
  
  return (
    <>
      <h1 className="display-4">Reading list</h1>
      <p>Drag and drop to change the order</p>
      {renderTable()}
      <Button onClick={()=>{updatePriorityList(); setDisabledSaveButton(true)}} variant="success" disabled={disabledSaveButton}>
        {disabledSaveButton ? "Saved" : "Save"}
      </Button>
    </>
  )

  function renderTable() {
    return (
      <DragDropContext onDragEnd={(result) => handleDragEnd(result)}>
        <Table className="table-striped border">
          <thead className="table-primary">
            <tr>
              <th></th>
              <th>Title</th>
              <th>Author</th>
              <th>Options</th>
            </tr>
          </thead>
          <Droppable droppableId="tbody">
            {(provided) =>
              <tbody ref={provided.innerRef} {...provided.droppableProps}>
                {booksPrority.map((bp, index) =>
                  <Draggable draggableId={bp.book.title} key={bp.book.title} index={index}>
                    {(provided) => 
                      <tr key={bp.priority} ref={provided.innerRef} {...provided.draggableProps}>
                        <td {...provided.dragHandleProps}><List /></td>
                        <td>
                          <Link to={`/books/${bp.bookId}`}>
                            {bp.book.title}
                          </Link>
                        </td>
                        <td>{bp.book.author}</td>
                        <td className="">
                          <Button onClick={()=>removeBookFromPriorityList(bp.bookId, true)} variant="outline-success" className="ms-2">
                            Mark as read and remove
                          </Button>
                          <Button onClick={()=>removeBookFromPriorityList(bp.bookId, false)} variant="outline-danger" className="ms-2">
                            Remove
                          </Button>
                        </td>
                      </tr>
                    }
                  </Draggable>
                )}
                {provided.placeholder}
              </tbody>
            }
          </Droppable>
        </Table>
      </DragDropContext>
    )
  }

  function getBookPriorityList() {
    fetch("/api/bookPriority", { method: 'GET' })
      .then(response => response.json())
      .then(json => setBooksPrority(json));
  }

  function updatePriorityList() {
    let priorityList = [];
    for (let i = 0; i < booksPrority.length; i++) {
      priorityList.push({
        priority: i,
        bookId: booksPrority[i].bookId
      })
    }

    fetch('/api/bookpriority/', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(priorityList)
    });
  }

  function removeBookFromPriorityList(bookId, isRead) {
    fetch(`/api/bookPriority/${bookId}?isRead=${isRead}`, { method: 'DELETE' });
    window.location.reload(false);
  }
}

export default ReadingList