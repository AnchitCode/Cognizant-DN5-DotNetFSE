const officeSpaces = [
  { name: 'Sky Tower', location: 'Bengaluru', rent: 45000, address: 'MG Road', image: 'https://images.unsplash.com/photo-1497366754035-f200968a6e72?auto=format&fit=crop&w=1200&q=80' },
  { name: 'Metro Hub', location: 'Hyderabad', rent: 72000, address: 'HITEC City', image: 'https://images.unsplash.com/photo-1524758631624-e2822e304c36?auto=format&fit=crop&w=1200&q=80' },
  { name: 'Harbor Point', location: 'Chennai', rent: 58000, address: 'OMR', image: 'https://images.unsplash.com/photo-1497366412874-3415097a27e7?auto=format&fit=crop&w=1200&q=80' },
];

function App() {
  return (
    <main>
      <h1>Office Space Rental</h1>
      <div>
        {officeSpaces.map(({ name, location, rent, address, image }) => {
          const rentStyle = { color: rent > 60000 ? 'green' : 'red' };
          
          return (
            <article key={name}>
              <img src={image} alt={name} width="300" />
              <div>
                <h2>{name}</h2>
                <p>Location: {location}</p>
                <p>Address: {address}</p>
                <p style={rentStyle}>Rent: {rent}</p>
              </div>
            </article>
          );
        })}
      </div>
    </main>
  );
}

export default App;
