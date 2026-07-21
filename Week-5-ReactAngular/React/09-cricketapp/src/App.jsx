import './App.css'

const squad = [
  { name: 'Rohit', role: 'Batsman', runs: 72 },
  { name: 'Sky', role: 'Batsman', runs: 48 },
  { name: 'Jadeja', role: 'All-rounder', runs: 31 },
  { name: 'Bumrah', role: 'Bowler', runs: 6 },
]

const topScorers = squad.filter(({ runs }) => runs >= 30).map(({ name, runs }) => ({
  name,
  runs,
}))

function App() {
  const { name: captain } = squad[0]
  const extendedSquad = [...squad, { name: 'Gill', role: 'Batsman', runs: 54 }]

  return (
    <main className="cricket-app">
      <section className="cricket-card">
        <p className="kicker">Cricket squad</p>
        <h1>{captain} leads the lineup</h1>
        <p>Array methods and destructuring shape the display below.</p>

        <div className="stats-row">
          {extendedSquad.map(({ name, runs }) => (
            <article key={name}>
              <strong>{name}</strong>
              <span>{runs} runs</span>
            </article>
          ))}
        </div>

        <div className="lists-grid">
          <section>
            <h2>Playing XI</h2>
            <ul>
              {extendedSquad.map(({ name, role }) => (
                <li key={name}>
                  {name} - {role}
                </li>
              ))}
            </ul>
          </section>

          <section>
            <h2>Top Scorers</h2>
            <ul>
              {topScorers.map(({ name, runs }) => (
                <li key={name}>
                  {name}: {runs}
                </li>
              ))}
            </ul>
          </section>
        </div>
      </section>
    </main>
  )
}

export default App
