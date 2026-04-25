// TodoApp JavaScript — Micro-interactions & UX enhancements

document.addEventListener('DOMContentLoaded', function () {

    // Auto-dismiss toast notifications
    const toasts = document.querySelectorAll('.toast-notification');
    toasts.forEach(toast => {
        setTimeout(() => {
            toast.style.display = 'none';
        }, 3200);
    });

    // Toggle description field visibility
    const toggleDescBtn = document.getElementById('toggleDescription');
    const descField = document.getElementById('descriptionField');
    if (toggleDescBtn && descField) {
        toggleDescBtn.addEventListener('click', function () {
            descField.classList.toggle('d-none');
            const icon = this.querySelector('i');
            if (descField.classList.contains('d-none')) {
                icon.className = 'bi bi-plus-lg';
            } else {
                icon.className = 'bi bi-dash-lg';
                descField.querySelector('textarea')?.focus();
            }
        });
    }

    // Checkbox toggle with animation
    document.querySelectorAll('.toggle-form').forEach(form => {
        const checkmark = form.closest('.todo-item')?.querySelector('.checkmark');
        if (checkmark) {
            checkmark.addEventListener('click', function () {
                this.style.animation = 'checkPop 0.3s ease';
                setTimeout(() => form.submit(), 150);
            });
        }
    });

    // Delete confirmation via modal
    let deleteForm = null;
    document.querySelectorAll('.btn-delete-trigger').forEach(btn => {
        btn.addEventListener('click', function () {
            deleteForm = this.closest('.delete-form');
            const todoTitle = this.getAttribute('data-title');
            const modalTitle = document.getElementById('deleteTodoTitle');
            if (modalTitle) {
                modalTitle.textContent = todoTitle;
            }
            const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
            modal.show();
        });
    });

    const confirmDeleteBtn = document.getElementById('confirmDelete');
    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener('click', function () {
            if (deleteForm) {
                deleteForm.submit();
            }
        });
    }

    // Add subtle staggered animation to todo items
    document.querySelectorAll('.todo-item').forEach((item, index) => {
        item.style.animationDelay = `${index * 0.04}s`;
    });

    // Focus on title input when page loads (if available)
    const titleInput = document.getElementById('todoTitle');
    if (titleInput && window.innerWidth > 768) {
        titleInput.focus();
    }
});
