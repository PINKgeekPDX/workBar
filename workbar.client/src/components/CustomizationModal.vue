<template>
  <div class="modal">
    <div class="modal-content">
      <div class="modal-header">
        <h2>Customize Toolbar Buttons</h2>
        <span class="close" @click="closeModal">&times;</span>
      </div>
      <div class="modal-body">
        <form @submit.prevent="saveCustomIcons">
          <div v-for="button in customizableButtons" :key="button.id" class="button-config">
            <label :for="`icon-${button.id}`">Button {{ button.id }}:</label>
            <input :id="`icon-${button.id}`"
                   :name="`icon-${button.id}`"
                   type="text"
                   v-model="button.icon"
                   placeholder="Enter new icon (e.g., 🔄)" />
          </div>
        </form>
      </div>
      <div class="modal-footer">
        <button type="button" class="cancel-button" @click="closeModal">Cancel</button>
        <button type="button" class="save-button" @click="saveCustomIcons">Save</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CustomizationModal',
  props: {
    buttons: {
      type: Array,
      required: true
    }
  },
  computed: {
    customizableButtons() {
      return this.buttons.filter(button => button.customizable);
    }
  },
  methods: {
    closeModal() {
      this.$emit('close');
    },
    saveCustomIcons() {
      const customIcons = {};
      this.customizableButtons.forEach(button => {
        customIcons[button.id] = button.icon;
      });
      localStorage.setItem('customToolbarIcons', JSON.stringify(customIcons));
      this.$emit('save', customIcons);
      this.closeModal();
    }
  }
};
</script>

<style scoped>
  .modal {
    display: flex;
    position: fixed;
    z-index: 1001;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgba(0,0,0,0.5);
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background-color: #2c3e50;
    padding: 20px;
    border: 1px solid #444;
    border-radius: 8px;
    width: 80%;
    max-width: 400px;
    color: #ecf0f1;
    box-shadow: 0 5px 15px rgba(0,0,0,0.3);
    animation: fadeInModal 0.3s ease;
  }

  @keyframes fadeInModal {
    from {
      opacity: 0;
      transform: translateY(-20px);
    }

    to {
      opacity: 1;
      transform: translateY(0);
    }
  }

  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

    .modal-header h2 {
      margin: 0;
      font-size: 1.5em;
    }

  .close {
    color: #ecf0f1;
    font-size: 1.5em;
    font-weight: bold;
    cursor: pointer;
  }

  .modal-body {
    margin-top: 10px;
  }

  .button-config {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
  }

    .button-config label {
      flex: 1;
    }

    .button-config input {
      flex: 2;
      padding: 5px;
      border: none;
      border-radius: 4px;
      font-size: 1em;
    }

  .modal-footer {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
    margin-top: 10px;
  }

    .modal-footer button {
      padding: 5px 10px;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      font-size: 1em;
    }

  .save-button {
    background-color: #27ae60;
    color: #ffffff;
  }

  .cancel-button {
    background-color: #c0392b;
    color: #ffffff;
  }
</style>
