# Azure Machine Learning Hands on Labs

### **Suggested timeline for Azure Machine Learning Hands On Lab (HOL)**

| Time (min) | Activity |
| ---        | ---      |
| 50         | [Introduction to Machine Learning](./000-into-machine-learning.pptx) |
| 20         | Lab1 - [Setting up development environment](./001-lab-setup.md) |
| 45         | Lab2 - [Introduction to R, Python & Data Synth](./002-lab-data-synth.md) |
| 45         | Lab3 - [AzureML Experiments & Data Interaction](./003-lab-data-interact.md) |
| 60         | Lab4 - [Develop and Consume AzureML Models](./004-lab-azureml-experiment.md) |
| 45         | Lab5 - [Custom Scripts (R & Python) in AML](./005-lab-custom-script-r-python.md) |
| 60         | Lab6 - [Evaluate model performance in AML](./006-lab-model-evaluation.md) |
| 60         | Lab7 - [Azure ML Batch Score, Retrain, Production and Automatization](./007-lab-production-ops.md) |


### **Detailed contents of the HOL**

 - [Introduction to Machine Learning](./000-into-machine-learning.pptx)
    
1. [Setting up development environment](./001-lab-setup.md)  
    * Overview
        * Objectives
        * Requirements
    * Create free tier Azure ML account  
    * Create standard tier Azure ML account  
    * Install R and R Studio  
    * Install Anaconda Python  

2. [Introduction to R, Python & Data Synth](./002-lab-data-synth.md)  
    * Overview
        * Objectives
        * Requirements
    * Generate Synthetic Data
        * Microsoft Excel
        * R
        * Python
        * Microsoft Azure SQL Server
        * Microsoft Azure Blob Storage
    * Other Dataset sources

3. [AzureML Experiments & Data Interaction](./003-lab-data-interact.md)  
    * Overview
        * Objectives
        * Requirements
    * Creating AzureML Experiment
    * Accessing Data
        * Access data, use existing dataset
        * Upload your own dataset
        * Upload your own compressed dataset
        * Manually enter data
        * Access data on Azure Storage
        * Access data on Azure SQL Database

4. [Develop and Consume AzureML Models](./004-lab-azureml-experiment.md)
    * Overview
        * Objectives
        * Requirements
    * Working with AzureML Models
        * Training a model
        * Publishing a trained model as Web Service
        * Removing Web Service Redundant input & output parameters
        * Consume the ML Web Service in a C# application
        * Input data type

5. [Custom Scripts (R & Python) in AML](./005-lab-custom-script-r-python.md)
    * Overview
        * Objectives
        * Requirements
    * R & Python Script Modules
        * Using Execute R Script module
        * Using Python Script module
        * R & Python compatibility with Azure ML

6. [Evaluate model performance in AML](./006-lab-model-evaluation.md)
    * Overview
        * Objectives
        * Requirements
    * Performance evaluation
        * Splitting data
        * Scoring the model
        * Evaluate a Regression model
        * Evaluate more than one model
        * Cross Validation
    * Performance evaluation (cont.)
        * Evaluate a Binary classification model
        * Comparing two binary classification model
        * Cross Validation on Binary Classification
        * Evaluating a Multi-class classification model
    * Feature engineering
        * Which feature is or is not important?
        * Simpler method to measure a feature’s importance

7. [Azure ML Batch Score, Retrain, Production and Automatization](./007-lab-production-ops.md)
    * Overview
        * Objectives
        * Requirements
    * Importance of Retraining, seeing the whole picture
    * Batch and Request/Response scoring web services
        * Stages to create a scoring web service
            * Request/Response Service (RRS)
            * Batch Execution Service (BES)
            * Web Service Input/Output Parameter alternatives
    * Azure ML Retraining
    