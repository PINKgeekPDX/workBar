<template>
  <div class="modal" @click.self="closeModal">
    <div class="modal-content">
      <div class="modal-header">
        <h2>Customize Buttons</h2>
        <span class="close" @click="closeModal">&times;</span>
      </div>
      <div class="modal-body">
        <form @submit.prevent="saveConfigurations">
          <div v-for="button in buttons" :key="button.id" class="button-config">
            <h3>Button {{ button.id }}</h3>
            <label>Icon:</label>
            <input v-model="button.icon" placeholder="Enter icon (e.g., 🏠)" />

            <label>Action Type:</label>
            <select v-model="button.actionType">
              <option value="url">Open URL</option>
              <option value="app">Launch Application</option>
              <option value="script">Execute Script</option>
              <option value="macro">Macro Playback</option>
            </select>

            <!-- Action Configurations -->
            <div v-if="button.actionType === 'url'">
              <label>URL:</label>
              <input v-model="button.actionConfig.url" placeholder="Enter URL" />
            </div>

            <div v-else-if="button.actionType === 'app'">
              <label>Application Path:</label>
              <input v-model="button.actionConfig.path" placeholder="Enter app path" />
              <label>Arguments:</label>
              <input v-model="button.actionConfig.arguments" placeholder="Enter arguments" />
              <label>
                <input type="checkbox" v-model="button.actionConfig.runAsAdmin" />
                Run as Administrator
              </label>
            </div>

            <div v-else-if="button.actionType === 'script'">
              <label>Script Path:</label>
              <input v-model="button.actionConfig.scriptPath" placeholder="Enter script path" />
              <label>Interpreter:</label>
              <select v-model="button.actionConfig.interpreter">
                <option value="powershell">PowerShell</option>
                <option value="cmd">CMD/BAT</option>
                <option value="python">Python</option>
              </select>
              <label>Arguments:</label>
              <input v-model="button.actionConfig.arguments" placeholder="Enter arguments" />
            </div>

            <div v-else-if="button.actionType === 'macro'">
              <label>Macro Sequence:</label>
              <textarea v-model="button.actionConfig.sequence" placeholder="Enter macro steps"></textarea>
            </div>

            <hr />
          </div>
          <div class="modal-footer">
            <button type="button" @click="closeModal">Cancel</button>
            <button type="submit">Save</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    props: ['buttons'],
    methods: {
      closeModal() {
        this.$emit('close');
      },
      saveConfigurations() {
        // Create a deep copy to avoid mutating the prop directly
        const updatedButtons = JSON.parse(JSON.stringify(this.buttons));
        this.$emit('save', updatedButtons);
        this.closeModal();
      },
    },
  };
</script>

<style scoped>
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background-color: #2c3e50;
    padding: 20px;
    width: 80%;
    max-width: 600px;
    border-radius: 8px;
    color: #ecf0f1;
  }

  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .modal-body {
    margin-top: 10px;
  }

  .button-config {
    margin-bottom: 20px;
  }

  .modal-footer {
    display: flex;
    justify-content: flex-end;
    gap: 10px;
  }

  .close {
    cursor: pointer;
    font-size: 1.5em;
  }

  label {
    display: block;
    margin-top: 10px;
  }

  input, select, textarea {
    width: 100%;
    padding: 5px;
    margin-top: 5px;
    border: none;
    border-radius: 4px;
  }

  textarea {
    resize: vertical;
    height: 80px;
  }
</style>
