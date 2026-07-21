import '../Stylesheets/mystyle.css'

function CalculateScore({ Name, School, Total, Goal }) {
  const percentage = ((Total / Goal) * 100).toFixed(2)

  return (
    <section className="score-card">
      <h1>Calculate Score</h1>
      <dl>
        <div>
          <dt>Name</dt>
          <dd>{Name}</dd>
        </div>
        <div>
          <dt>School</dt>
          <dd>{School}</dd>
        </div>
        <div>
          <dt>Total</dt>
          <dd>{Total}</dd>
        </div>
        <div>
          <dt>Goal</dt>
          <dd>{Goal}</dd>
        </div>
        <div>
          <dt>Percentage</dt>
          <dd>{percentage}%</dd>
        </div>
      </dl>
    </section>
  )
}

export default CalculateScore