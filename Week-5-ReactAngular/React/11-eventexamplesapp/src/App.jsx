import { useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [name, setName] = useState('')
  const [amount, setAmount] = useState('100')
  const [currency, setCurrency] = useState('USD')

  const increase = () => setCount((current) => current + 1)
  const decrease = () => setCount((current) => current - 1)
  const sayWelcome = () => alert('Welcome to the event examples app')
  const handleSyntheticEvent = (event) => setName(event.target.value)
  const converted = currency === 'USD' ? (Number(amount) / 83.2).toFixed(2) : (Number(amount) * 83.2).toFixed(2)

  return (
    <main className="events-app">
      <section className="events-card">
        <p className="kicker">Event examples</p>
        <h1>Interactive handlers</h1>

        <div className="counter-row">
          <p>Counter: {count}</p>
          <button type="button" onClick={increase}>
            Increase
          </button>
          <button type="button" onClick={decrease}>
            Decrease
          </button>
          <button
            type="button"
            onClick={() => {
              increase()
              increase()
            }}
          >
            Increase Twice
          </button>
        </div>

        <div className="example-grid">
          <section>
            <h2>Say welcome</h2>
            <button type="button" onClick={sayWelcome}>
              Say Welcome
            </button>
          </section>

          <section>
            <h2>Synthetic event</h2>
            <input type="text" value={name} onChange={handleSyntheticEvent} placeholder="Type your name" />
            <p>Hello, {name || 'guest'}</p>
          </section>

          <section>
            <h2>Currency converter</h2>
            <input type="number" value={amount} onChange={(event) => setAmount(event.target.value)} />
            <select value={currency} onChange={(event) => setCurrency(event.target.value)}>
              <option value="USD">USD</option>
              <option value="INR">INR</option>
            </select>
            <p>
              Converted amount: {converted} {currency === 'USD' ? 'USD' : 'INR'}
            </p>
          </section>
        </div>
      </section>
    </main>
  )
}

export default App
