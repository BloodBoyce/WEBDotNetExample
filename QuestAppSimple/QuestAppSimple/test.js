import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    // Parameters for traffic density
    vus: 10, // 10 Virtual USers
    duration: '10s', // Test runtime
};
// !!PORT MIGHT NEED TO BE ADJUSTED!!
const BASE_URL = 'http://localhost:5162/api/questions';

export default function () {
    // 1. Fetch all questions
    let getRes = http.get(BASE_URL);
    check(getRes, {
        'GET /questions is 200': (r) => r.status === 200,
    });

    // 2. Add a new question
    // let postRes = http.post(BASE_URL, null, { params: { content: 'Is K6 working?' } });
    let postRes = http.post(`${BASE_URL}?content=${encodeURIComponent("K6: Loadtest Question")}`);
    check(postRes, {
        'POST /questions is 200': (r) => r.status === 200,
    });
    
    sleep(1); // Pause between cycles
    // duration & sleep determine frequency of calls
    // VUS determines how many calls run simultaneously
}
