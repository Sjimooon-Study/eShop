import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
	vus: 300,
	duration: '60s',
};

export default function () {
	// Make http request and sleep for 1 second.
	http.get("https://localhost:44373/Locomotives?QueryOptions.SearchString=DB&Scales=0&Controls=2&LocoTypes=0&Epochs=3&QueryOptions.OrderByOptions=0&QueryOptions.PageSize=10");
	sleep(1);
}
