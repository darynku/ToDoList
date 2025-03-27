<script setup>
import { ref, onMounted } from 'vue'
import { notesService } from './services/notesService'

const notes = ref([])
const newNoteTitle = ref('')
const newNoteDescription = ref('')
const newNoteCategory = ref('')
const newNoteDueDate = ref('')
const loading = ref(false)
const error = ref(null)
const currentPage = ref(1)
const pageSize = ref(10)
const totalPages = ref(1)
const totalItems = ref(0)
const showModal = ref(false)
const noteToComplete = ref(null)

const loadNotes = async () => {
  try {
    loading.value = true
    error.value = null
    const data = await notesService.getAllNotes(currentPage.value, pageSize.value)
    notes.value = data.items
    totalPages.value = data.totalPages
    totalItems.value = data.totalItems
  } catch (err) {
    error.value = err.message
    console.error(err)
  } finally {
    loading.value = false
  }
}

const changePage = (page) => {
  currentPage.value = page
  loadNotes()
}

const addNote = async () => {
  if (newNoteTitle.value.trim() && newNoteDescription.value.trim()) {
    try {
      loading.value = true
      error.value = null
      await notesService.addNote({
        title: newNoteTitle.value,
        description: newNoteDescription.value,
        category: newNoteCategory.value
      })
      newNoteTitle.value = ''
      newNoteDescription.value = ''
      newNoteCategory.value = ''
      newNoteDueDate.value = ''
      await loadNotes()
    } catch (err) {
      error.value = 'Ошибка при добавлении заметки'
      console.error(err)
    } finally {
      loading.value = false
    }
  }
}

const toggleNote = async (id) => {
  try {
    loading.value = true
    error.value = null
    await notesService.completeNote(id)
    await loadNotes()
  } catch (err) {
    error.value = 'Ошибка при обновлении статуса заметки'
    console.error(err)
  } finally {
    loading.value = false
  }
}

const deleteNote = async (id) => {
  try {
    loading.value = true
    error.value = null
    await notesService.deleteNote(id)
    await loadNotes()
  } catch (err) {
    error.value = 'Ошибка при удалении заметки'
    console.error(err)
  } finally {
    loading.value = false
  }
}

const updateCategory = async (id, category) => {
  try {
    loading.value = true
    error.value = null
    await notesService.updateCategory(id, category)
    await loadNotes()
  } catch (err) {
    error.value = 'Ошибка при обновлении категории'
    console.error(err)
  } finally {
    loading.value = false
  }
}

const updateDueDate = async (id, dueDate) => {
  try {
    loading.value = true
    error.value = null
    await notesService.updateDueDate(id, dueDate)
    await loadNotes()
  } catch (err) {
    error.value = 'Ошибка при обновлении даты'
    console.error(err)
  } finally {
    loading.value = false
  }
}

const formatDate = (dateString) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  return date.toLocaleDateString('ru-RU', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });
}

const confirmComplete = (id) => {
  noteToComplete.value = id
  showModal.value = true
}

const completeNote = async () => {
  if (noteToComplete.value) {
    try {
      loading.value = true
      error.value = null
      await notesService.completeNote(noteToComplete.value)
      await loadNotes()
      showModal.value = false
      noteToComplete.value = null
    } catch (err) {
      error.value = 'Ошибка при завершении задачи'
      console.error(err)
    } finally {
      loading.value = false
    }
  }
}

onMounted(loadNotes)
</script>

<template>
  <div class="app-container">
    <header>
      <h1>✨ Мои Заметки</h1>
      <p class="subtitle">Организуйте свои мысли и задачи</p>
    </header>
    
    <div class="input-container">
      <div class="input-group">
        <div class="input-row">
          <input 
            v-model="newNoteTitle" 
            placeholder="Заголовок заметки..."
            type="text"
            title="Введите заголовок заметки"
          >
          <input 
            v-model="newNoteCategory" 
            placeholder="Категория"
            type="text"
            title="Введите категорию заметки"
          >
        </div>
        <textarea
          v-model="newNoteDescription"
          placeholder="Описание заметки..."
          rows="3"
          title="Введите описание заметки"
        ></textarea>
        <div class="input-row">
          <input 
            v-model="newNoteDueDate" 
            type="datetime-local"
            title="Выберите срок выполнения"
          >
          <button @click="addNote" :disabled="loading">Добавить</button>
        </div>
      </div>
    </div>

    <div v-if="error" class="error-message">
      {{ error }}
    </div>

    <div class="notes-grid">
      <div 
        v-for="note in notes" 
        :key="note.id" 
        class="note-card"
        :class="{ 'completed': note.isCompleted }"
      >
        <div class="note-content">
          <div class="note-header">
            <h3>{{ note.title }}</h3>
            <span class="category" v-if="note.category">{{ note.category }}</span>
          </div>
          <p class="note-description">{{ note.description }}</p>
          <div class="note-details">
            <p v-if="note.dueDate" class="due-date">
              Срок: {{ formatDate(note.dueDate) }}
            </p>
            <small class="created-date">
              Создано: {{ formatDate(note.created) }}
            </small>
          </div>
        </div>
        <div class="note-actions">
          <div class="action-group">
            <input 
              v-if="!note.isCompleted"
              type="text" 
              :value="note.category"
              @change="updateCategory(note.id, $event.target.value)"
              placeholder="Категория"
            >
            <input 
              v-if="!note.isCompleted"
              type="datetime-local"
              :value="note.dueDate"
              @change="updateDueDate(note.id, $event.target.value)"
            >
          </div>
          <div class="action-buttons">
            <button 
              v-if="!note.isCompleted"
              @click="confirmComplete(note.id)" 
              class="toggle-btn complete-btn" 
              :disabled="loading"
            >
              Завершить
            </button>
            <button @click="deleteNote(note.id)" class="delete-btn" :disabled="loading">🗑️</button>
          </div>
        </div>
      </div>
    </div>

    <div class="pagination" v-if="totalPages > 1">
      <button 
        @click="changePage(currentPage - 1)" 
        :disabled="currentPage === 1"
        class="pagination-btn"
      >
        ←
      </button>
      <span class="page-info">
        Страница {{ currentPage }} из {{ totalPages }}
      </span>
      <button 
        @click="changePage(currentPage + 1)" 
        :disabled="currentPage === totalPages"
        class="pagination-btn"
      >
        →
      </button>
    </div>

    <div v-if="loading" class="loading-overlay">
      <div class="loading-spinner"></div>
    </div>

    <!-- Модальное окно подтверждения -->
    <div v-if="showModal" class="modal-overlay">
      <div class="modal">
        <h3 style="color: black;">Подтверждение</h3>
        <p style="color: black;">Вы уверены, что хотите завершить эту задачу?</p>
        <button @click="completeNote" style="margin-top: 1.5rem;">Да</button>
        <button @click="showModal = false" style="margin-top: 1.5rem;">Нет</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.app-container {
  max-width: 1400px;
  margin: 0 auto;
  padding: 2rem;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  min-height: 100vh;
  background-color: #ffffff;
}

header {
  text-align: center;
  margin-bottom: 3rem;
}

h1 {
  color: #1a1a1a;
  font-size: 3rem;
  margin-bottom: 0.5rem;
  font-weight: 700;
}

.subtitle {
  color: #4a4a4a;
  font-size: 1.2rem;
}

.input-container {
  background: #f8f8f8;
  padding: 2rem;
  border-radius: 24px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  margin-bottom: 2rem;
  border: 1px solid #e0e0e0;
}

.input-group {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.input-row {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 1rem;
}

input, textarea {
  padding: 1rem;
  border: 2px solid #e0e0e0;
  border-radius: 12px;
  font-size: 1rem;
  transition: all 0.3s ease;
  background: white;
  color: #1a1a1a;
}

input::placeholder, textarea::placeholder {
  color: #757575;
}

textarea {
  resize: vertical;
  min-height: 100px;
}

input:focus, textarea:focus {
  outline: none;
  border-color: #4CAF50;
  box-shadow: 0 0 0 3px rgba(76, 175, 80, 0.1);
}

button {
  padding: 1rem 2rem;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 12px;
  cursor: pointer;
  font-size: 1rem;
  transition: all 0.3s ease;
  font-weight: 500;
}

button:hover:not(:disabled) {
  background-color: #45a049;
  transform: translateY(-1px);
}

button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.notes-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(400px, 1fr));
  gap: 1.5rem;
}

.note-card {
  background: #f8f8f8;
  padding: 1.5rem;
  border-radius: 24px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  transition: all 0.3s ease;
  border: 1px solid #e0e0e0;
}

.note-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
}

.note-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 1rem;
}

.note-header h3 {
  margin: 0;
  font-size: 1.2rem;
  color: #1a1a1a;
}

.note-description {
  color: #4a4a4a;
  margin: 0.5rem 0;
  line-height: 1.5;
}

.category {
  background: #e8f5e9;
  padding: 0.25rem 0.75rem;
  border-radius: 9999px;
  font-size: 0.875rem;
  color: #2e7d32;
}

.note-details {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.due-date {
  color: #d32f2f;
  font-size: 0.875rem;
  margin: 0;
  font-weight: 500;
}

.created-date {
  color: #757575;
  font-size: 0.75rem;
}

.note-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 1rem;
  padding-top: 1rem;
  border-top: 1px solid #e0e0e0;
}

.action-group {
  display: flex;
  gap: 0.5rem;
}

.action-group input {
  padding: 0.5rem;
  font-size: 0.875rem;
}

.action-buttons {
  display: flex;
  gap: 0.5rem;
}

.toggle-btn, .delete-btn {
  padding: 0.5rem;
  background: none;
  border: none;
  cursor: pointer;
  font-size: 1.2rem;
  transition: transform 0.2s ease;
}

.toggle-btn:hover:not(:disabled), .delete-btn:hover:not(:disabled) {
  transform: scale(1.1);
}

.completed {
  background-color: #f5f5f5;
  opacity: 0.8;
}

.completed h3, .completed .note-description {
  text-decoration: line-through;
  color: #9e9e9e;
}

.error-message {
  background: #ffebee;
  color: #d32f2f;
  padding: 1rem;
  border-radius: 12px;
  margin-bottom: 1rem;
  text-align: center;
}

.loading-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(255, 255, 255, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.loading-spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #e0e0e0;
  border-top-color: #4CAF50;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

@media (max-width: 1200px) {
  .input-row {
    grid-template-columns: 1fr;
  }
  
  .notes-grid {
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  }
}

@media (max-width: 768px) {
  .app-container {
    padding: 1rem;
  }
  
  h1 {
    font-size: 2rem;
  }
  
  .note-card {
    padding: 1rem;
  }
  
  .action-group {
    flex-direction: column;
  }
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-top: 2rem;
  padding: 1rem;
  background: #f8f8f8;
  border-radius: 12px;
  border: 1px solid #e0e0e0;
}

.pagination-btn {
  padding: 0.5rem 1rem;
  background: #4CAF50;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 1.2rem;
  transition: all 0.3s ease;
}

.pagination-btn:hover:not(:disabled) {
  background: #45a049;
  transform: translateY(-1px);
}

.pagination-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-info {
  color: #4a4a4a;
  font-size: 1rem;
}

/* Стили для модального окна */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.modal button {
  margin-top: 1.5rem; /* Увеличено расстояние между кнопками */
  padding: 0.5rem 1rem;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.modal button:hover {
  background-color: #45a049;
}

.complete-btn {
  padding: 0.5rem 1rem; /* Уменьшено расстояние */
  background-color: #4CAF50; /* Постоянно зеленая */
  color: white;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.complete-btn:hover {
  background-color: #45a049;
}
</style>
