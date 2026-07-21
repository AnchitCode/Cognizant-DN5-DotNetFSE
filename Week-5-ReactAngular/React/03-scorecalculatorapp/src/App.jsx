import './App.css'
import CalculateScore from './Components/CalculateScore.jsx'

function App() {
  return (
    <main className="score-app">
      <CalculateScore Name="Aarav" School="Cognizant Academy" Total={438} Goal={500} />
    </main>
  )
}

export default App
