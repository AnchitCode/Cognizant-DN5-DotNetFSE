import { useState } from 'react'
import './App.css'

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [booking, setBooking] = useState(false)

  const flights = ['AI 203 - Mumbai to Delhi', '6E 421 - Bengaluru to Goa', 'UK 877 - Chennai to Kochi']

  return (
    <main className="ticket-app">
      <section className="ticket-card">
        <p className="kicker">Ticket booking</p>
        <h1>{isLoggedIn ? 'Welcome back' : 'Guest access'}</h1>
        <p>
          {isLoggedIn
            ? 'Choose a flight and confirm your booking.'
            : 'Log in to access flight booking options.'}
        </p>

        <div className="action-row">
          {isLoggedIn ? (
            <button type="button" onClick={() => setIsLoggedIn(false)}>
              Logout
            </button>
          ) : (
            <button type="button" onClick={() => setIsLoggedIn(true)}>
              Login
            </button>
          )}
        </div>

        {isLoggedIn ? (
          <div className="booking-panel">
            <h2>Available flights</h2>
            <ul>
              {flights.map((flight) => (
                <li key={flight}>
                  {flight}
                  <button type="button" onClick={() => setBooking(true)}>
                    Book
                  </button>
                </li>
              ))}
            </ul>
            {booking && <p className="success">Your ticket has been reserved.</p>}
          </div>
        ) : (
          <div className="booking-panel muted">
            <h2>Guest view</h2>
            <p>Login to see flights and place a booking.</p>
          </div>
        )}
      </section>
    </main>
  )
}

export default App
