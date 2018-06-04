# Create Sample Data ------------------------------------------------------
# 1k rows
# ProductName | eAge (18-50) | Gender (M,F) | PurchasePrice (0.25 - 1.25)
# CoconutWater| 24 | F | 1.00
# CoconutWater| 24 | M | 0.50

# Given Purchase Age + Gender + ProductName predict the PurchasePrice

ages <- c(18:50)
prices <- c(0.25, 0.35, 0.50, 0.75, 0.99, 1.00, 1.25)
genders <- factor(c("M", "F"))
products <- factor(c("coconut water", "soda", "water"))

distAges <- function(x) {
	if (x > 17 & x < 30) {
		rep(x, 10)
	} else {
		x
	}
}

generateExample <- function(index) {
	age <- sample(18:50, 1)
	if (17 < age && age < 30) {
		purchasePrice <- prices[max(sample(1:length(prices) - 1, 1), 1)]
		if (purchasePrice < 0.75) {
			productSelected <- products[sample(2:3, 1)]
		} else {
			productSelected <- products[sample(1:2, 1)]
		}
	}
	else {
		purchasePrice <- prices[max(sample(4:length(prices) - 1, 1), 1)]
		if (purchasePrice < 0.99) {
			productSelected <- products[sample(2:3, 1)]
		} else {
			productSelected <- products[sample(1:2, 1)]
		}
	}

	gender <- genders[sample(1:2, 1)]
	result <- list(age = age, purchasePrice = purchasePrice, gender = gender, productSelected = productSelected)
	return(result)
}

#create 1000 examples
res <- lapply(1:1000, generateExample)

# convert the row to a dataframe
sampleData <- do.call(rbind.data.frame, res)

# assign to the output dataset
data.set = sampleData;

# Select data.frame to be sent to the output Dataset port
maml.mapOutputPort("data.set");

# END Create Sample Data ------------------------------------------------------



# Build the Model ---------------------------------------------------------------
# Input: dataset
# Output: model

model <- lm(purchasePrice ~ age + gender + productSelected, data = dataset)
# END Build the Model



# Score an example using the model------------------------------------------------
# Input: model, dataset
# Output: scores

# Score an example using the model------------------------------------------------
prediction <- predict(model, dataset)
summary(prediction)

scores <- data.frame(prediction)
# END Score an example

