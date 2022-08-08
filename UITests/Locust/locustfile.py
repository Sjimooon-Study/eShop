from locust import HttpUser, task

class TestClass(HttpUser):
    @task
    def get_Items(self):
        self.client.verify = False
        self.client.get("/Locomotives")
