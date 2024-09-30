#Задание 3

apiVersion: apps/v1 <br/>
kind: Deployment <br/>
metadata:<br/>
  name: web-app<br/>
spec:<br/>
  replicas: 3  # Мы указываем три реплики, так как они справляются с нагрузкой.<br/>
  selector:<br/>
    matchLabels:<br/>
      app: web-app<br/>
  template:<br/>
    metadata:<br/>
      labels:<br/>
        app: web-app<br/>
    spec:<br/>
      containers:<br/>
      - name: web-container<br/>
        image: your-app-image:latest  # Замените на образ вашего приложения.<br/>
        resources:<br/>
          requests:<br/>
            memory: "128Mi"  # Запрос минимальных ресурсов памяти.<br/>
            cpu: "100m"      # Нужный уровень CPU для ровной работы.<br/>
          limits:<br/>
            memory: "128Mi"<br/>
            cpu: "500m"      # Ограничение по CPU для инициализации.<br/>
        readinessProbe:<br/>
          httpGet:<br/>
            path: /health  # Замените на ваш путь для проверки готовности.<br/>
            port: 8080<br/>
          initialDelaySeconds: 10  # Учитывая, что инициализация занимает 5-10 секунд.<br/>
          periodSeconds: 5<br/>
        livenessProbe:<br/>
          httpGet:<br/>
            path: /health  # Лайвнесс-проба для проверки состояния контейнера.<br/>
            port: 8080<br/>
          initialDelaySeconds: 20<br/>
          periodSeconds: 10<br/>
      affinity:<br/>
        podAntiAffinity:  # Обеспечиваем, что поды будут распределены по разным нодам для отказоустойчивости.<br/>
          requiredDuringSchedulingIgnoredDuringExecution:<br/>
          - labelSelector:<br/>
              matchExpressions:<br/>
              - key: app<br/>
                operator: In<br/>
                values:<br/>
                - web-app
            topologyKey: "kubernetes.io/hostname"<br/>

# HorizontalPodAutoscaler для автоматического скейлинга при изменении нагрузки<br/>
---<br/>
apiVersion: autoscaling/v2<br/>
kind: HorizontalPodAutoscaler<br/>
metadata:<br/>
  name: web-app-hpa<br/>
spec:<br/>
  scaleTargetRef:<br/>
    apiVersion: apps/v1<br/>
    kind: Deployment<br/>
    name: web-app<br/>
  minReplicas: 3  # Минимум 3 реплики.<br/>
  maxReplicas: 5  # При увеличении нагрузки можно добавить реплики.<br/>
  metrics:<br/>
  - type: Resource<br/>
    resource:<br/>
      name: cpu<br/>
      target:<br/>
        type: Utilization<br/>
        averageUtilization: 50  # Средняя загрузка CPU для скейлинга.<br/>
