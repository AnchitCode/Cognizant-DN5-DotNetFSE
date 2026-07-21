import { useState } from 'react';

const sections = {
  book: [
    { id: 1, title: 'Atomic Habits', author: 'James Clear' },
    { id: 2, title: 'Deep Work', author: 'Cal Newport' },
  ],
  blog: [
    { id: 1, title: 'Designing for focus', author: 'Asha' },
    { id: 2, title: 'Shipping with confidence', author: 'Ravi' },
  ],
  course: [
    { id: 1, title: 'React Fundamentals', duration: '6 weeks' },
    { id: 2, title: 'Advanced JavaScript', duration: '4 weeks' },
  ],
};

export default function App() {
  const [view, setView] = useState('book');
  const content = sections[view];

  let courseContent = null;
  if (view === 'course') {
    courseContent = (
      <ul>
        {content.map((item) => (
          <li key={item.id}>
            <strong>{item.title}</strong>
            <span>{item.duration}</span>
          </li>
        ))}
      </ul>
    );
  }

  return (
    <main>
      <h1>Blogger Hub</h1>
      
      <div>
        <button type="button" onClick={() => setView('book')}>Book Details</button>
        <button type="button" onClick={() => setView('blog')}>Blog Details</button>
        <button type="button" onClick={() => setView('course')}>Course Details</button>
      </div>

      <div>
        {view === 'book' && (
          <ul>
            {content.map((item) => (
              <li key={item.id}>
                <strong>{item.title}</strong>
                <span>{item.author}</span>
              </li>
            ))}
          </ul>
        )}

        {view === 'blog' ? (
          <ul>
            {content.map((item) => (
              <li key={item.id}>
                <strong>{item.title}</strong>
                <span>{item.author}</span>
              </li>
            ))}
          </ul>
        ) : null}

        {courseContent}
      </div>
    </main>
  );
}
