import CohortDetails from './CohortDetails';

const cohorts = [
  { name: 'React Basics', mentor: 'Lakshmi', status: 'Ongoing' },
  { name: 'Hooks Sprint', mentor: 'Arjun', status: 'Completed' },
  { name: 'State Management', mentor: 'Neha', status: 'Pending' },
];

function App() {
  return (
    <main>
      <h1>Training cohorts</h1>
      <div>
        {cohorts.map((cohort) => (
          <CohortDetails key={cohort.name} cohort={cohort} />
        ))}
      </div>
    </main>
  );
}

export default App;
