import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [todos, setTodos] = useState([]);
    const [content, setContent] = useState('');
    const [editingId, setEditingId] = useState(null);
    const [editingContent, setEditingContent] = useState('');

    const apiBase = 'https://localhost:44342/api/todo';

    useEffect(() => {
        fetchTodos();
    }, []);

    const fetchTodos = async () => {
        const res = await fetch(apiBase);
        const data = await res.json();
        setTodos(data);
    };

    const addTodo = async () => {
        console.log("Add button clicked");

        if (!content.trim()) return alert("Content can't be empty");

        await fetch(apiBase, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ content })
        });
        setContent('');
        fetchTodos();
    };

    const updateTodo = async (todo) => {
        await fetch(`${apiBase}/${todo.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(todo)
        });
        setEditingId(null);
        fetchTodos();
    };

    const toggleComplete = async (todo) => {
        await updateTodo({ ...todo, isCompleted: !todo.isCompleted });
    };

    const deleteTodo = async (id) => {
        await fetch(`${apiBase}/${id}`, { method: 'DELETE' });
        fetchTodos();
    };

    return (
        <div className="container">
            <h1>To-do List</h1>
            <div className="input-group">
                <input
                    type="text"
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    placeholder="Add new to-do"
                />
                <button onClick={addTodo}>Add</button>
            </div>
            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        <input
                            type="checkbox"
                            checked={todo.isCompleted}
                            onChange={() => toggleComplete(todo)}
                        />
                        {editingId === todo.id ? (
                            <>
                                <input
                                    type="text"
                                    value={editingContent}
                                    onChange={(e) => setEditingContent(e.target.value)}
                                />
                                <div className="todo-actions">
                                    <button onClick={() => updateTodo({ ...todo, content: editingContent })}>
                                        Save
                                    </button>
                                    <button onClick={() => setEditingId(null)}>Cancel</button>
                                </div>
                            </>
                        ) : (
                            <>
                                <span
                                    className="todo-text"
                                    style={{ textDecoration: todo.isCompleted ? 'line-through' : 'none' }}
                                >
                                    {todo.content}
                                </span>
                                <div className="todo-actions">
                                    <button onClick={() => {
                                        setEditingId(todo.id);
                                        setEditingContent(todo.content);
                                    }}>Edit</button>
                                    <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                                </div>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
