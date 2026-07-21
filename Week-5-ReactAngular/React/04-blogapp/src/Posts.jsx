import { Component } from 'react'
import Post from './Post'

class Posts extends Component {
  state = {
    posts: [],
    loading: true,
    error: null,
  }

  async componentDidMount() {
    try {
      const response = await fetch('https://jsonplaceholder.typicode.com/posts?_limit=6')
      if (!response.ok) {
        throw new Error('Unable to load posts')
      }

      const data = await response.json()
      const posts = data.map(({ id, title, body }) => new Post(id, title, body))

      this.setState({ posts, loading: false })
    } catch (error) {
      this.setState({ error: error.message, loading: false })
    }
  }

  componentDidCatch(error) {
    this.setState({ error: error.message, loading: false })
  }

  render() {
    const { posts, loading, error } = this.state

    if (loading) {
      return (
        <section className="loading-panel">
          <p className="status-pill">Fetching posts</p>
          <p>Loading the latest stories from JSONPlaceholder.</p>
        </section>
      )
    }

    if (error) {
      return (
        <section className="error-panel">
          <p className="status-pill">Load failed</p>
          <p>{error}</p>
        </section>
      )
    }

    return (
      <section className="blog-shell">
        <header className="blog-hero">
          <p className="status-pill">React class component</p>
          <h1>Blog posts</h1>
          <p>
            Posts are fetched in <strong>componentDidMount</strong> and rendered with a class-based
            component structure.
          </p>
        </header>

        <section className="posts-panel">
          <div className="posts-grid">
            {posts.map((post) => (
              <article key={post.id} className="post-card">
                <h2>{post.title}</h2>
                <p>{post.body}</p>
              </article>
            ))}
          </div>
        </section>
      </section>
    )
  }
}

export default Posts