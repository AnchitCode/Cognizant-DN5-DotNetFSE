import styles from './CohortDetails.module.css';

export default function CohortDetails({ cohort }) {
  const headingClass = cohort.status === 'Ongoing' ? styles.green : styles.blue;

  return (
    <div>
      <h3 className={headingClass}>{cohort.name}</h3>
      <p>Mentor: {cohort.mentor}</p>
      <span>{cohort.status}</span>
    </div>
  );
}
