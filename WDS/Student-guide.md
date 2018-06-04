![Microsoft Cloud Workshop](../media/ms-cloud-workshop.png "Microsoft Cloud Workshop")

[Legal notice](../legal.md)

Updated May 2018

# Intelligent vending machines whiteboard design session student guide

In this whiteboard design session, you will design an IoT solution for intelligent vending machines, leveraging facial feature recognition and Azure Machine Learning, to gain a better understanding of building cloud-based machine learning apps and real-time analytics with SQL Database in-memory and columnar indexing.

## Contents

* [Step 1: Review the customer case study](#step-1-review-the-customer-case-study)
  * [Customer situation](#customer-situation)
  * [Customer needs](#customer-needs)
  * [Customer objections](#customer-objections)
  * [Infographic for common scenarios](#infographic-for-common-scenarios)
* [Step 2: Design a proof of concept solution](#step-2-design-a-proof-of-concept-solution)
* [Step 3: Present the solution](#step-3-present-the-solution)
* [Wrap-up](#wrap-up)
* [Additional references](#additional-references)

## Step 1: Review the customer case study

**Outcome**: Analyze your customer’s needs.

**Time frame**: 15 minutes

**Directions**: With all participants in the session, the facilitator/SME presents an overview of the customer case study along with technical tips.

1. Meet your table participants and trainer
2. Read all of the directions for steps 1–3 in the student guide
3. As a table team, review the following customer case study

### Customer situation

Trey Research Inc. looks at the old way of doing things in retail, and introduces innovative experiences that delight customers and drive sales. Their latest initiative focuses on intelligent vending machines that have sophisticated computing platforms called Vending Machine IO boards (which are capable of running Microsoft Windows). These boards are well advanced beyond the traditional Vending Machine Controller (VMC) that are only capable of rudimentary functions like controlling temperature, dispensing product and processing cash payment. Each vending machine includes a large, hi-definition display, a touch screen, a camera, and peripherals for handling cash and credit cards, and they are all connected via either WIFI or 4G LTE connections to the Internet.

They work with chipset vendors and manufacturers to build the specialized units, so instead the bulk of their effort goes into the software and the platform that make the vending machines intelligent. In their current initiative they are looking at designing a solution that addresses three core areas: commerce, engagement analytics and intelligent promotions.

When it comes to commerce, they are looking at modernizing the handling of purchase transactions - their vending machines accept payment by cash or credit card, and in the future, may accept payment by Near Field Communication (NFC). When an item is purchased, a form of payment is captured (e.g., cash is inserted, or a credit card is inserted). In dealing with a cash payment, the currency received is verified at the vending machine. The currency is returned if not accepted and the purchase may be aborted by the consumer. Credit card payments require an additional step. They need to authorize and capture payment first via a 3rd party payment gateway. Again, the purchase may be aborted by the consumer if the credit card is declined. When the purchase transaction completes successfully, Trey needs to update their centralized inventory. This inventory, which describes what is in-stock for each vending machine, is adjusted down to reflect the item sold at that machine. Trey would like to ensure that regardless of the payment method or the transaction outcome, that all purchase transactions are logged to a database for later analysis.

While commerce is of prime importance, Trey Research believes that understanding the events that led to a successful or unsuccessful purchase transaction are just as important. They want to be able to collect Engagement Telemetry that includes dwell times, impressions and conversions. These are defined as follows:

* **Dwell Times**: a user "session", for a given subject. Dwell times indicate how long the user either interacted with the device or stood by examining the inventory/promotions. These metrics have a start time and duration.
* **Impressions**: the time and type of promotion displayed
* **Conversions**: when a recommendation is displayed, and a purchase transaction occurred, was the recommended product purchased or not?

The goal being that the data that their vending machine provides can paint a picture of the purchase funnel---that is understanding what products were viewed in a session, which was ultimately purchased, and which was not?

The final piece of their Intelligent Vending Machine trifecta (as Grant Trey, their Chief Innovation Officer puts it) is Intelligent Promotion. Each vending machine maintains a local copy of the ads (images, videos) that are appropriate to the inventory and used with promotions. These are pushed as a package down to the vending machine on-demand. The vending machine gets a notification indicating new packages are available and then downloads them from the repository.

"It should be like being able to give the vending machine a new paint job with the click of a button", says Dan Cole their VP of Engineering.

New Packages are configured in the database (via a web-based portal) and are then scheduled for download by the vending machines. There is a website that is used to create new promotions and schedule them for downloading to machines.

With the advertisement and signage in place, Trey wants their vending machines to show off a little in the way it approaches promotions. A promotion selects a single product to feature by displaying its ad signage and presents it to the consumer with a discounted price, e.g. it is priced "Just-for-you". The way Trey Research envisions this, when a vending machine identifies a visitor in proximity, it takes a photo that it will use to anonymously determine demographics (such as age, gender, and possibly features like whether the consumer is smiling or wearing sunglasses) that are used to decide what to promote on its display. This photo is uploaded to a web service, which then uses face detection to extract demographics data about the consumer. The demographics are sent to a recommendation model that suggests a product and a price to promote and the recommended promotion is returned from the call to the web service. The vending machine then displays the recommended product at the recommended price. In addition to driving the recommendation, these demographics (but not the photos) are stored durably for later analysis and for re-training of the recommendation model.

Trey Research realizes that their vending machines and the cloud are a perfect match, and are looking for ways to build their solution using Microsoft Azure. They are particularly interested in how they can deploy scalable machine learning models. Today they expect their training data to be in the tens of GB, but they want to ensure they pick a solution that can get to terabyte scale if needed. Finally, they are also particularly keen on a tool that can help them quickly explore, wrangle and visualize their data either with desktop or web based interfaces.

### Customer needs

1. An IoT solution that can handle high volumes of telemetry data, and enables the solution to communicate with the vending machines for situations like package updates.
2. A data store that can handle the extremely write-heavy workload that results from the purchase transactions, whilst still allowing them to quickly perform analytics using SQL.
3. A platform on which to build and train machine learning models against high volumes of training data, ideally programmed with R.
4. A solution that can provide demographics, given a photo of a person.
5. A highly scalable storage solution that won't "max out" and can handle all the telemetry from their vending machines.
6. Tools for performing light-weight wrangling of their data, exploration and visualization.

### Customer objections

1. We've heard that Azure's machine learning can only train on data sets up to 10GB in size, are we blocked?
2. While not required in the short term, would our machine learning approach enable us to support reinforcement learning (whereby recommendations that lead to a purchase are preferred over time)?
3. Can we really perform real-time analytics using only a single data store? We cannot afford to lose any of our purchase transaction data.
4. We are concerned that training our models will take too long.

### Infographic for common scenarios

![Screenshot of a sample Internet of Things workflow, which is broken into On-Premises and Azure services. At this time, we are unable to capture all of the information in the slide. Future versions of this course should address this.](./media/common-scenarios.png "Common scenarios for Internet of Things")

## Step 2: Design a proof of concept solution

**Outcome**: Design a solution and prepare to present the solution to the target customer audience in a 15-minute chalk-talk format.

**Time frame**: 60 minutes

### Business needs

**Directions**: With all participants at your table, answer the following questions and list the answers on a flip chart.

1. Who should you present this solution to? Who is your target customer audience? Who are the decision makers?
2. What customer business needs do you need to address with your solution?

### Design

**Directions**: With all participants at your table, respond to the following questions on a flip chart.

#### High-level architecture

1. Without getting into the details (the following sections will address the details), diagram your initial vision for handling the top-level requirements for data loading, data preparation, storage, machine learning modeling, and reporting. You will refine this diagram as you proceed.

#### Commerce

1. How would you recommend that Trey complete purchase transactions and store their purchase transaction history in Azure?
2. What services would you suggest and how would configure the storage and indexing?

#### Engagement analytics

1. What service would you recommend Trey capitalize on to scalably ingest the engagement telemetry directly from the vending machines?
2. Would you recommend they use Azure Storage or Azure Data Lake Store for persisting their engagement telemetry? Be specific with your reasoning.
3. What is processing the telemetry ingested, at least in so far as persisting the telemetry to the durable storage you recommended? How is this configured or implemented?

#### Facial demographics

1. What Azure service or API would you suggest Trey utilize for determining demographics about a consumer from a photo of them taken by the vending machine?

#### Intelligent promotions

1. What technology would you recommend that Trey use for implementing their machine learning model that recommends a product and price given consumer demographics?
2. How would you guide Trey to load data, so it can be used for training the machine learning model?
3. What category of machine learning algorithm would you recommend to Trey for use in constructing their model? For this scenario, your options are: clustering, regression or two-class classification. Why?
4. Are there any R packages you would suggest they utilize?
5. How would you operationalize your trained model, so it can be invoked with the demographics?
6. Where would you store the packages containing promotional artifacts for download by the vending machine and how would you instruct the vending machine to download and install them? Be specific on any Azure services and protocols used.

#### Visualization and reporting

1. What tool would you recommend Trey utilize for performing ad-hoc wrangling, exploration and visualization of their data?
2. How would you make the resulting visualization available to others in the organization?

### Prepare

**Directions**: With all participants at your table:

1. Identify any customer needs that are not addressed with the proposed solution.
2. Identify the benefits of your solution.
3. Determine how you will respond to the customer’s objections.
4. Prepare a 15-minute chalk-talk style presentation to the customer.

## Step 3: Present the solution

**Outcome**: Present a solution to the target customer audience in a 15-minute chalk-talk format.

**Time frame**: 30 minutes

### Presentation

**Directions**:

1. Pair with another table.
2. One table is the Microsoft team and the other table is the customer.
3. The Microsoft team presents their proposed solution to the customer.
4. The customer makes one of the objections from the list of objections.
5. The Microsoft team responds to the objection.
6. The customer team gives feedback to the Microsoft team.
7. Tables switch roles and repeat Steps 2–6.

## Wrap-up

**Timeframe**: 15 minutes

Tables reconvene with the larger group to hear a SME share the preferred solution for the case study.

## Additional references

|   |   |
|----------|-------------|
| **Description** | **Link** |
|Creating ARM templates|<https://docs.microsoft.com/azure/azure-resource-manager/resource-manager-create-first-template>|
| Machine Learning | <https://azure.microsoft.com/documentation/articles/machine-learning-algorithm-choice/> |
|Machine Learning Server|<https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server>|
| Real-time Analytics | <https://msdn.microsoft.com/library/dn817827.aspx/> |
| R Server on HDInsight | <https://azure.microsoft.com/documentation/articles/hdinsight-hadoop-r-server-overview/> |
| Power BI | <https://powerbi.microsoft.com/what-is-power-bi/> |