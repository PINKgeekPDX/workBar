import axios from 'axios';

const apiClient = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  launchApp(config) {
    return apiClient.post('/buttons/launch-app', config);
  },
  executeScript(config) {
    return apiClient.post('/buttons/execute-script', config);
  },
  // Add other API methods as needed
};
