import React from 'react'
import { Link } from 'react-router-dom'

function Home() {
  return (
    <div className="text-center">
      <h1 className="display-4 mb-4">Home</h1>
      <p>
        This project allows user to add <Link to="/books">books</Link> and <Link to="/authors">authors</Link> to database and create own <Link to="reading">reading list</Link>
      </p>
      <p>
        You can find the description of this project on <a href="https://github.com/ZaleX0/ReadingList">github</a>
      </p>
    </div>
  )
}

export default Home