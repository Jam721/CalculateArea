#Задание 3

apiVersion: apps/v1
kind: Deployment
metadata:
  name: web-app
spec:
  replicas: 3  # Мы указываем три реплики, так как они справляются с нагрузкой.
  selector:
    matchLabels:
      app: web-app
  template:
    metadata:
      labels:
        app: web-app
    spec:
      containers:
      - name: web-container
        image: your-app-image:latest  # Замените на образ вашего приложения.
        resources:
          requests:
            memory: "128Mi"  # Запрос минимальных ресурсов памяти.
            cpu: "100m"      # Нужный уровень CPU для ровной работы.
          limits:
            memory: "128Mi"
            cpu: "500m"      # Ограничение по CPU для инициализации.
        readinessProbe:
          httpGet:
            path: /health  # Замените на ваш путь для проверки готовности.
            port: 8080
          initialDelaySeconds: 10  # Учитывая, что инициализация занимает 5-10 секунд.
          periodSeconds: 5
        livenessProbe:
          httpGet:
            path: /health  # Лайвнесс-проба для проверки состояния контейнера.
            port: 8080
          initialDelaySeconds: 20
          periodSeconds: 10
      affinity:
        podAntiAffinity:  # Обеспечиваем, что поды будут распределены по разным нодам для отказоустойчивости.
          requiredDuringSchedulingIgnoredDuringExecution:
          - labelSelector:
              matchExpressions:
              - key: app
                operator: In
                values:
                - web-app
            topologyKey: "kubernetes.io/hostname"

# HorizontalPodAutoscaler для автоматического скейлинга при изменении нагрузки
---
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: web-app-hpa
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: web-app
  minReplicas: 3  # Минимум 3 реплики.
  maxReplicas: 5  # При увеличении нагрузки можно добавить реплики.
  metrics:
  - type: Resource
    resource:
      name: cpu
      target:
        type: Utilization
        averageUtilization: 50  # Средняя загрузка CPU для скейлинга.
