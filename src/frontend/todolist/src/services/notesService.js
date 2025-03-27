const API_URL = 'http://localhost:5001/api/notes';

export const notesService = {
  async getAllNotes(page = 1, pageSize = 10) {
    const response = await fetch(`${API_URL}?page=${page}&pageSize=${pageSize}`);
    if (!response.ok) {
      throw new Error('Ошибка при загрузке заметок');
    }
    const data = await response.json();
    return data;
  },

  async getNoteById(id) {
    const response = await fetch(`${API_URL}/${id}`);
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Заметка не найдена');
      }
      throw new Error('Ошибка при загрузке заметки');
    }
    return response.json();
  },

  async addNote(noteData) {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title: noteData.title,
        description: noteData.description,
        category: noteData.category
      }),
    });
    if (!response.ok) {
      throw new Error('Ошибка при добавлении заметки');
    }
    return true;
  },

  async updateNote(id, noteData) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title: noteData.title,
        description: noteData.description,
        isCompleted: noteData.isCompleted,
        dueDate: noteData.dueDate,
        category: noteData.category
      }),
    });
    if (!response.ok) {
      throw new Error('Ошибка при обновлении заметки');
    }
    return true;
  },

  async completeNote(id) {
    const response = await fetch(`${API_URL}/${id}/complete`, {
      method: 'PATCH',
    });
    if (!response.ok) {
      throw new Error('Ошибка при обновлении статуса заметки');
    }
    return true;
  },

  async updateDueDate(id, dueDate) {
    const response = await fetch(`${API_URL}/${id}/dueDate`, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ 
        newDueDate: dueDate ? new Date(dueDate).toISOString() : null 
      }),
    });
    if (!response.ok) {
      throw new Error('Ошибка при обновлении даты');
    }
    return true;
  },

  async updateCategory(id, category) {
    const response = await fetch(`${API_URL}/${id}/category`, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ category }),
    });
    if (!response.ok) {
      throw new Error('Ошибка при обновлении категории');
    }
    return true;
  },

  async deleteNote(id) {
    const response = await fetch(`${API_URL}/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Заметка не найдена');
      }
      throw new Error('Ошибка при удалении заметки');
    }
    return true;
  },
}; 